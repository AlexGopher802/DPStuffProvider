package com.example.dpstuffproviderstore.models

import com.google.gson.annotations.SerializedName

data class OrderData (
        @SerializedName("id") var id : Int?,
        @SerializedName("address") var address : String?,
        @SerializedName("lastName") var lastName : String?,
        @SerializedName("firstName") var firstName : String?,
        @SerializedName("phone") var phone : String?,
        @SerializedName("frontDoor") var frontDoor : Int?,
        @SerializedName("apartNum") var apartNum : Int?,
        @SerializedName("floorNum") var floorNum : Int?,
        @SerializedName("intercom") var intercom : String?,
        @SerializedName("deliveryDate") var deliveryDate : String?,
        @SerializedName("timeFrom") var timeFrom : String?,
        @SerializedName("timeTo") var timeTo : String?,
        @SerializedName("commentary") var commentary : String?,
        @SerializedName("idCourier") var idCourier : String?,
        @SerializedName("summ") var summ : Double?,
        @SerializedName("priority") var priority : Int?,
        @SerializedName("status") var status : String?,
        @SerializedName("listProducts") var listProducts : List<ProductData>?
)