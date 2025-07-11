package model

import (
)


type Activity struct {
    Type            string                  `json:"type"`
	Status          string                  `json:"status"`
    Date            *DateOnly               `json:"date"`
    Notes           string                  `json:"notes"`
}