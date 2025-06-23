package handler

import (
	"net/http"

	"github.com/gin-gonic/gin"
	"github.com/matrjones/ReptiRealm/backend/Go/internal/service"
)

func GetReptiles(c *gin.Context) {
	reptiles, err := service.GetAllReptiles()
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": "unable to fetch reptiles"})
		return
	}
	c.JSON(http.StatusOK, reptiles)
}
