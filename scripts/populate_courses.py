import os
import pandas as pd
from dotenv import load_dotenv
from sqlalchemy import create_engine

load_dotenv()
server = os.getenv('DB_SERVER')
database = os.getenv('DB_DATABASE')
username = os.getenv('DB_USERNAME')
password = os.getenv('DB_PASSWORD')

conn_str = f"mssql+pyodbc://{username}:{password}@{server}/{database}?driver=ODBC+Driver+18+for+SQL+Server&TrustServerCertificate=yes"
engine = create_engine(conn_str)

courses_column_mappings = {
    'course_code': 'CourseCode',
    'title': 'CourseTitle',
    'credits': 'Credits',
    'type_name': 'CourseType',
    'objective': 'Objective',
    'teaching_methods': 'TeachingMethods',
    'content': 'Content',
    'learning_material': 'LearningMaterial',
    'teaching_method': 'LocationType',
    'qualifications': 'Qualifications',
    'employer_connections': 'EmployerConnections',
    'exam_schedule': 'ExamSchedule',
    'international_connections': 'InternationalConnections',
    'workload': 'Workload',
    'content_scheduling': 'ContentScheduling',
    'course_information': 'CourseInformation',
    'further_information': 'FurtherInformation',
    'evaluation_scale': 'EvaluationScale',
    'unit_title': 'FacultyName',
    'teaching_method_online': 'OnlineCredits',
    'teaching_method_contact': 'ContactCredits',
    'min_seats': 'MinSeats',
    'max_seats': 'MaxSeats',
}

teachers_column_mappings = {
    'name': 'Name',
}

junction_column_mappings = {
    'teacher_id': 'TeacherId',
    'course_code': 'CourseCode',
}

courses_df = pd.read_csv('../data/courses.csv')
courses_df.rename(columns=courses_column_mappings, inplace=True)

teachers_df = pd.read_csv('../data/teachers.csv')
teachers_df.rename(columns=teachers_column_mappings, inplace=True)

junction_df = pd.read_csv('../data/junction.csv')
junction_df.rename(columns=junction_column_mappings, inplace=True)

def df_to_sql(df, table_name, engine):
    df.to_sql(table_name, con=engine, if_exists='append', index=False)

# Insert data into Courses table from DataFrame
df_to_sql(courses_df, 'Courses', engine)

# Insert data into Teachers table from DataFrame
df_to_sql(teachers_df, 'Teachers', engine)

# Insert data into TeacherCourses table (Junction table) from DataFrame
df_to_sql(junction_df, 'TeacherCourses', engine)