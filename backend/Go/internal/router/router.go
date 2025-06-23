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
	reptile := router.Group("/reptiles")
	{
		reptile.GET("", reptileController.GetReptiles)
		reptile.GET("/:id", reptileController.GetReptileById)
		//reptile.POST("", reptileController.PostReptile)
	}

    //User Endpoints
	user := router.Group("/users")
	{
		user.GET("", userController.GetUsers)
		user.GET("/:id", userController.GetUserById)
		user.POST("", userController.PostUser)
	}

	return r
}