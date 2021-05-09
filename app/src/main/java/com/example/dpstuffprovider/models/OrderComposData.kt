package com.example.dpstuffprovider.models

import com.google.gson.annotations.SerializedName
import java.io.Serializable

data class OrderComposData (
    @SerializedName("name") var name : String,
    @SerializedName("shopName") var shopName : String,
    @SerializedName("quantity") var quantity : Int,
    @SerializedName("price") var price : Int
) : Serializable

data class OrderCompos(
    var listOrderCompos : List<OrderComposData>
) : Serializable