package com.example.dpstuffproviderstore.models
import com.google.gson.annotations.SerializedName

data class CategoryData (
    @SerializedName("id") var id : Int,
    @SerializedName("name") var name : String,
    @SerializedName("parentName") var parentName : String,
    @SerializedName("imageUrl") var imageUrl : String
)