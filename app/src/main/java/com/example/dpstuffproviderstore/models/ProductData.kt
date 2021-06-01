package com.example.dpstuffproviderstore.models

import com.google.gson.annotations.SerializedName

data class ProductData (
        @SerializedName("id") var id : Int,
        @SerializedName("name") var name : String,
        @SerializedName("price") var price : Double?,
        @SerializedName("rating") var rating : Double?,
        @SerializedName("avail") var avail : Boolean?,
        @SerializedName("category") var category : String,
        @SerializedName("store") var store : String,
        @SerializedName("quantity") var quantity : Int? = 1
)