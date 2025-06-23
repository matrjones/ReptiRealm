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

	cursor, err := db.MongoDatabase.Collection("reptiles").Find(ctx, bson.M{})
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
	err := db.MongoDatabase.Collection("reptiles").FindOne(ctx, bson.M{"_id": id}).Decode(&reptile)
	if err != nil {
		return nil, err
	}
	return &reptile, nil
}