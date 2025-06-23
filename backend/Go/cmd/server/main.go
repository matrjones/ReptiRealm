package main

import (
	"log"
	"os"
	"github.com/matrjones/ReptiRealm/backend/Go/config"
	"github.com/matrjones/ReptiRealm/backend/Go/internal/db"
	"github.com/matrjones/ReptiRealm/backend/Go/internal/router"
)

func main() {
	cfg := config.Load()	

	db.InitMongo(cfg)

	r := router.SetupRouter()
	port := cfg.Port
	if port == "" {
		port = "8080"
	}

	log.Fatal(r.Run(":" + port))
}