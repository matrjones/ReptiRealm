package db

import(
	"context"
    "fmt"

    "go.mongodb.org/mongo-driver/mongo"
    "go.mongodb.org/mongo-driver/mongo/options"

	"github.com/matrjones/ReptiRealm/backend/Go/config"
)

var MongoClient *mongo.Client
var MongoDatabase *mongo.Database

func InitMongo(cfg *config.Config) {
	ctx, cancel := context.WithTimeout(context.Background(), 10*time.Second)
	defer cancel()

	clientOptions := options.Client().ApplyURI(cfg.MongoURI)
	client, err := mongo.Connect(ctx, clientOptions)
	if err != nil {
		log.Fatalf("Mongo connect error: %v", err)
	}

	MongoClient = client
	MongoDatabase = client.Database(cfg.MongoDB)
}