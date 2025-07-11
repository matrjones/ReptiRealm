package router

import (
	"github.com/gin-gonic/gin"
	"github.com/matrjones/ReptiRealm/backend/Go/internal/handler"
	//"github.com/matrjones/ReptiRealm/backend/Go/internal/middleware"
)

func SetupRouter() *gin.Engine {
	r := gin.Default()

	// Register global middleware
	//r.Use(middleware.AuthMiddleware())

	// API routes
	reptile := r.Group("/reptiles")
	{
		reptile.GET("", handler.GetReptiles)
		reptile.GET("/:id", handler.GetReptileById)
		reptile.GET("/getByName/:name", handler.GetReptilesByName)
		reptile.POST("", handler.PostReptile)
		reptile.PUT("/:id", handler.UpdateReptile)
		reptile.POST("/:id/activities", handler.AddActivityToReptile)
	}

	return r
}

