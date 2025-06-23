package service

import (
	"github.com/matrjones/ReptiRealm/backend/Go/internal/model"
	"github.com/matrjones/ReptiRealm/backend/Go/internal/repository"
)

func GetAllReptiles() ([]model.Reptile, error) {
	return repository.GetReptiles()
}

func GetReptileById() ([]model.Reptile, error) {
	return repository.GetReptiles()
}