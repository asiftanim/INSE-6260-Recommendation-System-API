import pandas as pd
import pyodbc
import sqlalchemy as sql
from flask import Flask, jsonify, Response

app = Flask(__name__)

server = 'localhost' 
database = 'rrs' 
username = 'SA' 
password = 'Sqa@12345' 
driver = '{ODBC Driver 18 for SQL Server}'

cnxn = pyodbc.connect('DRIVER={ODBC Driver 18 for SQL Server};SERVER='+server+';DATABASE='+database+';ENCRYPT=no;UID='+username+';PWD='+ password)
query = "SELECT * from dbo.RestaurantsVisited"
data = None

@app.route("/<id>")
def get_recommendation(id):
    generate_recommendation(id)
    try:
        return Response(generate_recommendation(id), mimetype='application/json')
    except Exception:
        return Response("[]", mimetype='application/json')

def generate_recommendation(id):
    #data = pd.read_csv('restaurents_visited.csv')
    data = pd.read_sql(query, cnxn)

    # Create user-item matrix
    matrix = data.pivot_table(index='UserId', columns='PlaceId', values='Rating')

    # User similarity matrix using Pearson correlation
    user_similarity = matrix.T.corr()

    # Pick a user ID
    picked_userid = id

    # Remove picked user ID from the candidate list
    user_similarity.drop(index=picked_userid, inplace=True)

    # Number of similar users
    n = 100

    # User similarity threashold
    user_similarity_threshold = 0.5

    # Get top n similar users
    similar_users = user_similarity[user_similarity[picked_userid]>user_similarity_threshold][picked_userid].sort_values(ascending=False)
    
    # Movies that the target user has watched
    picked_userid_watched = matrix[matrix.index == picked_userid].dropna(axis=1, how='all')

    # Movies that similar users watched. Remove movies that none of the similar users have watched
    similar_user_movies = matrix[matrix.index.isin(similar_users.index)].dropna(axis=1, how='all')

    # Remove the watched movie from the movie list
    similar_user_movies.drop(picked_userid_watched.columns,axis=1, inplace=True, errors='ignore')

    # Average rating for the picked user
    avg_rating = matrix[matrix.index == picked_userid].T.mean()[picked_userid]

    # A dictionary to store item scores
    item_score = {}

    # Loop through items
    for i in similar_user_movies.columns:
        # Get the ratings for movie i
        movie_rating = similar_user_movies[i]
        # Create a variable to store the score
        total = 0
        # Create a variable to store the number of scores
        count = 0
        # Loop through similar users
        for u in similar_users.index:
            # If the movie has rating
            if pd.isna(movie_rating[u]) == False:
                # Score is the sum of user similarity score multiply by the movie rating
                score = similar_users[u] * movie_rating[u]
                # Add the score to the total score for the movie so far
                total += score
                # Add 1 to the count
                count +=1
        # Get the average score for the item
        item_score[i] = total / count

    # Convert dictionary to pandas dataframe
    item_score = pd.DataFrame(item_score.items(), columns=['PlaceId', 'Rating'])
        
    # Sort the movies by score
    ranked_item_score = item_score.sort_values(by='Rating', ascending=False)

    # Calcuate the predicted rating
    ranked_item_score['PredictedRating'] = ranked_item_score['Rating'] + avg_rating

    # Take a look at the data
    m = 10
    #print(ranked_item_score.head(m))
    
    print(len(ranked_item_score.index))
    if(len(ranked_item_score.index) < 10):
        #pr = get_popular_restaurants(10, ranked_item_score.loc[:,"movie"].astype(str))
        popular_restaurant = data.groupby('PlaceId').agg(Rating = ('Rating', 'count'), PredictedRating = ('Rating', 'mean')).reset_index()
        pr = popular_restaurant.head(10 - len(ranked_item_score.index))
        frames = [ranked_item_score, pr]
        merged_ranked_item_score = pd.concat(frames)
        merged_ranked_item_score.reset_index(drop=True, inplace=True)
        merged_ranked_item_score.index += 1
        merged_ranked_item_score['Rank'] = merged_ranked_item_score.index
        print(print("Popular/Mix"))
        return merged_ranked_item_score.to_json(orient='records')
    else:
        ranked_item_score.reset_index(drop=True, inplace=True)
        ranked_item_score.index += 1
        ranked_item_score['Rank'] = ranked_item_score.index
        print("All Recommended")
        return ranked_item_score.head(10).to_json(orient='records')