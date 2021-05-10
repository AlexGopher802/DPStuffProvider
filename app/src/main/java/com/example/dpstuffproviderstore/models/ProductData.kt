package com.example.dpstuffproviderstore.models

import com.google.gson.annotations.SerializedName

data class ProductData (
        @SerializedName("id") var id : Int,
        @SerializedName("name") var name : String,
        @SerializedName("price") var price : Int,
        @SerializedName("rating") var rating : Int,
        @SerializedName("avail") var avail : Boolean,
        @SerializedName("category") var category : String,
        @SerializedName("store") var store : String
)