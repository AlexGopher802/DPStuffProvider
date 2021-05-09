package com.example.dpstuffprovider.models

import com.google.gson.annotations.SerializedName
import java.io.Serializable

data class CouriersData (
    @SerializedName("id") var id : Int,
    @SerializedName("lastName") var lastName : String,
    @SerializedName("firstName") var firstName : String,
    @SerializedName("patronymic") var patronymic : String,
    @SerializedName("login") var login : String,
    @SerializedName("password") var password : String,
    @SerializedName("orderQuantity") var orderQuantity : Int,
    @SerializedName("phone") var phone : String,
    @SerializedName("email") var email : String
) : Serializable