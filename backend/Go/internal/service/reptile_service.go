package service

import (
	"github.com/matrjones/ReptiRealm/backend/Go/internal/model"
	"github.com/matrjones/ReptiRealm/backend/Go/internal/repository"
	"go.mongodb.org/mongo-driver/bson/primitive"
)

func GetAllReptiles() ([]model.Reptile, error) {
	return repository.GetReptiles()
}

func GetReptileById(id primitive.ObjectID) (*model.Reptile, error) {
	return repository.GetReptileById(id)
}

func GetReptilesByName(name string) ([]model.Reptile, error) {
	return repository.GetReptilesByName(name)
}

func PostReptile(reptile *model.Reptile) (*model.Reptile, error) {
	return repository.PostReptile(reptile)
}