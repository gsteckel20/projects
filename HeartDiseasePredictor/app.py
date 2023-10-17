from flask import Flask,render_template,request,session,jsonify
import sqlite3
import pandas as pd
import numpy as np
import joblib
import json
from sklearn.compose import ColumnTransformer
from sklearn.pipeline import Pipeline
from sklearn.impute import SimpleImputer
from sklearn.preprocessing import FunctionTransformer
from sklearn.preprocessing import StandardScaler, OneHotEncoder
from sklearn.ensemble import RandomForestClassifier
from sklearn.model_selection import train_test_split
from sklearn.metrics import accuracy_score
from sklearn.metrics import confusion_matrix
import matplotlib.pyplot as plt
import seaborn as sns

import joblib



app = Flask(__name__)
app.secret_key = 'key'
#Building the pipeline outside of a route so it can be accessed by all routes
def process_dataframe(df):
        df = df.replace('?', np.nan)

        imputer = SimpleImputer(strategy='constant', fill_value=0)
        imputer.fit(df)
        df = pd.DataFrame(imputer.fit_transform(df), columns=df.columns)
        df = df.astype(int)
    
        return df

custom_transformer = FunctionTransformer(func=process_dataframe, validate=False)

pipeline = Pipeline([
    ('preprocess', custom_transformer),
    ('model', RandomForestClassifier())
    ])

@app.route('/')
def home():
    conn = sqlite3.connect('heartdisease.db')
    c = conn.cursor()
    #getting the data from database
    X = pd.read_sql("SELECT * FROM heart_disease", conn)
    y = pd.read_sql("SELECT * FROM heart_disease_target", conn)
    numerical_features = ['age', 'trestbps', 'chol', 'thalach', 'oldpeak']
    categorical_features = ['sex', 'cp', 'fbs', 'restecg', 'exang', 'slope', 'ca', 'thal']
    X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

    pipeline.fit(X_train, y_train)
    joblib.dump(pipeline, 'model_pipeline.pkl')

    # Make predictions on test data
    y_pred = pipeline.predict(X_test)
    y_train_pred = pipeline.predict(X_train)
   # Evaluate the model
    confusion = confusion_matrix(y_test,y_pred)
    confusion_list = confusion.tolist()
    session['confusion'] = confusion_list
    #Make custom accuracy method
    def custom_accuracy(y_true, y_pred):
        y_true = y_true.values
     #Ensure y_true and y_pred have the same length
        if len(y_true) != len(y_pred):
            raise ValueError("Input arrays must have the same length.")
    
        correct_predictions = 0
        total_predictions = len(y_true)
    
        for i in range(total_predictions):
            true_label = y_true[i]
            pred_label = y_pred[i]
            if true_label == 0 and pred_label == 0:
                correct_predictions += 1
            elif true_label in [1, 2, 3, 4] and pred_label in [1, 2, 3, 4]:
                correct_predictions += 1
        print(correct_predictions)
        accuracy = correct_predictions / total_predictions
        return accuracy
    accuracy = custom_accuracy(y_test, y_pred)
    session['accuracy'] = accuracy
    accuracy1 = accuracy_score(y_train, y_train_pred)
    return render_template('my_index.html')

@app.route('/submit',methods=['GET','POST'])
def submit():
    if request.method == 'POST':
        #Get all entered values
        age = request.form.get('age')
        sex = request.form.get('sex')
        cp = request.form.get('cp')
        trestbps = request.form.get('trestbps')
        chol = request.form.get('chol')
        fbs = request.form.get('fbs')
        restecg = request.form.get('restecg')
        thalach = request.form.get('thalach')
        exang = request.form.get('exang')
        oldpeak = request.form.get('oldpeak')
        oldpeak = float(oldpeak)
        slope = request.form.get('slope')
        ca = request.form.get('ca')
        thal = request.form.get('thal')
        #load pipeline
        loaded_pipeline = joblib.load('model_pipeline.pkl')
        data_dict = [{'age': age, 'sex': sex, 'cp': cp, 'trestbps': trestbps, 'chol': chol, 'fbs': fbs, 'restecg': restecg, 
              'thalach': thalach, 'exang': exang, 'oldpeak': oldpeak, 'slope': slope, 'ca': ca, 'thal': thal}]
        new_data = pd.DataFrame(data_dict)
        #Enter data in database
        conn = sqlite3.connect('heartdisease.db')
        c = conn.cursor()
        c.execute('''
        CREATE TABLE IF NOT EXISTS entered_data (
        age INTEGER,
        sex INTEGER,
        cp INTEGER,
        trestbps INTEGER,
        chol INTEGER,
        fbs INTEGER,
        restecg INTEGER,
        thalach INTEGER,
        exang INTEGER,
        oldpeak FLOAT,
        slope INTEGER,
        ca INTEGER,
        thal INTEGER
        )''')
        c.execute('''INSERT INTO entered_data (age, sex, cp, trestbps, chol, fbs, restecg, thalach, exang, oldpeak, slope, ca, thal)
        VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
        ''', (age, sex,cp, trestbps, chol, fbs, restecg, thalach, exang, oldpeak, slope, ca, thal))
        conn.commit()
        conn.close()
        #predict on the data
        new_prediction = loaded_pipeline.predict(new_data)
        if new_prediction == 0:
            diagnosis = "> 50% diameter narrowing"
        else:
            diagnosis = "< 50% diameter narrowing"
    return render_template('my_index.html', Diagnosis = diagnosis)
#Gets all accuracy
@app.route('/accuracy', methods=['GET','POST'])
def accuracy():
    if request.method == 'POST':
        return render_template('accuracy_page.html', accuracy = session.get('accuracy'), confusion = np.array(session.get('confusion')))


app.run(debug=True, port=8002)

#1. The fun part was creating the html form. I had trouble preprossing the data because of the question marks in it.
#I tryed to use a column transformer but I noticed that it wasn't handling the question marks in the data.
#I decided to use a function transformer instead so I could deal with the question marks before the simple imputer.
#2. I used a ramdom forest classifier because it reduces the risk of overfitting and improving generalization performance.
#It helps mitigate the problem of high variance associated with individual decision trees.
#3. The widget that lets you see your html page as you are creating it was very helpful.
#I also just had to run the website over and over again to debug errors dirring the development.