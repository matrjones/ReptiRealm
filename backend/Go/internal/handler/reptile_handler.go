package handler

import (
	"net/http"

	"github.com/gin-gonic/gin"
	"github.com/matrjones/ReptiRealm/backend/Go/internal/service"

	"go.mongodb.org/mongo-driver/bson/primitive"
)

func GetReptiles(c *gin.Context) {
	reptiles, err := service.GetAllReptiles()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "unable to fetch reptiles"})
		return
	}
	c.JSON(http.StatusOK, reptiles)
}

func GetReptileById(c *gin.Context) {
	
	id, err1 := primitive.ObjectIDFromHex(c.Param("id"))
	if err1 != nil {
		c.IndentedJSON(http.StatusBadRequest, gin.H{"message": err1.Error()})
		return
	}
	
	reptile, err2 := service.GetReptileById(id)
	if err2 != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "unable to fetch reptile"})
		return
	}
	c.JSON(http.StatusOK, reptile)
}

func GetReptilesByName(c *gin.Context) {

	name := c.Param("name")

	reptiles, err := service.GetReptilesByName(name)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "unable to fetch reptiles"})
		return
	}
	c.JSON(http.StatusOK, reptiles)
}
