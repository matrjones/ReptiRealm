package repository

import (
	"context"
	"time"

    "go.mongodb.org/mongo-driver/bson"
    "go.mongodb.org/mongo-driver/bson/primitive"
	
	"github.com/matrjones/ReptiRealm/backend/Go/internal/db"
	"github.com/matrjones/ReptiRealm/backend/Go/internal/model"
)

func GetReptiles() ([]model.Reptile, error){
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	cursor, err := db.MongoDatabase.Collection("Reptiles").Find(ctx, bson.M{})
	if err != nil {
        return nil, err
    }
	defer cursor.Close(ctx)

	var reptiles []model.Reptile
	err = cursor.All(ctx, &reptiles)
	if err != nil {
		return nil, err
	}
	return reptiles, nil
}

func GetReptileById(id primitive.ObjectID) (*model.Reptile, error) {
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	var reptile model.Reptile
	err := db.MongoDatabase.Collection("Reptiles").FindOne(ctx, bson.M{"_id": id}).Decode(&reptile)
	if err != nil {
		return nil, err
	}
	return &reptile, nil
}

func GetReptilesByName(name string) ([]model.Reptile, error) {
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	cursor, err := db.MongoDatabase.Collection("Reptiles").Find(ctx, bson.M{"name": name})
	if err != nil {
        return nil, err
	}
	defer cursor.Close(ctx)

	var reptiles []model.Reptile
	if err := cursor.All(ctx, &reptiles); err != nil {
		return nil, err
	}
	
	return reptiles, nil
}

func PostReptile(newReptile *model.Reptile) (*model.Reptile, error) {
	
	newReptile.ID = primitive.NewObjectID()

	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	if newReptile.Morphs == nil {
		newReptile.Morphs = []string{}
	}

	if newReptile.Activities == nil {
		newReptile.Activities = []model.Activity{}
	}

	_, err := db.MongoDatabase.Collection("Reptiles").InsertOne(ctx, newReptile)
	if err != nil {
		return nil, err
	}

	return newReptile, nil
}

func UpdateReptile(reptile *model.Reptile) (*model.Reptile, error) {
	
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	if reptile.Morphs == nil {
		reptile.Morphs = []string{}
	}

	if reptile.Activities == nil {
		reptile.Activities = []model.Activity{}
	}

	_, err := db.MongoDatabase.Collection("Reptiles").UpdateOne(ctx, bson.M{"_id": reptile.ID}, bson.M{"$set": reptile})
	if err != nil {
		return nil, err
	}

	return reptile, nil
}

func AddActivityToReptile(reptile *model.Reptile, activity *model.Activity) (*model.Reptile, error) {
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	update := bson.M{"$push": bson.M{"activities": activity}}
	_, err := db.MongoDatabase.Collection("Reptiles").UpdateOne(ctx, bson.M{"_id": reptile.ID}, update)
	if err != nil {
		return nil, err
	}

	// Optionally, fetch the updated reptile
	return GetReptileById(reptile.ID)
}
