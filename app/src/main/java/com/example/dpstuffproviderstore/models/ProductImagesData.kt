package com.example.dpstuffproviderstore.models

import com.google.gson.annotations.SerializedName

data class ProductImagesData (
        @SerializedName("id") var id : Int,
        @SerializedName("idProduct") var idProduct : Int,
        @SerializedName("imageUrl") var imageUrl : String
)