package model

import (
    "go.mongodb.org/mongo-driver/bson/primitive"
)


type Reptile struct {
    ID              primitive.ObjectID      `json:"id"  bson:"_id,omitempty"`
    Name            string                  `json:"name"`
    Sex             string                  `json:"sex"`
    Species         *string                 `json:"species"`
    Morphs          []string                `json:"morphs"`
    DateOfBirth     *DateOnly               `json:"dateOfBirth"`
    DateObtained    *DateOnly               `json:"dateObtained"`
    Activities      []Activity              `json:"activities"`
}