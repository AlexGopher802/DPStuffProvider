package com.example.dpstuffprovider.models

import com.google.gson.annotations.SerializedName
import java.io.Serializable

data class CouriersData (
    @SerializedName("Id") var id : Int,
    @SerializedName("LastName") var lastName : String,
    @SerializedName("FirstName") var firstName : String,
    @SerializedName("Patronymic") var patronymic : String,
    @SerializedName("Login") var login : String,
    @SerializedName("Password") var password : String,
    @SerializedName("OrderQuantity") var orderQuantity : Int,
    @SerializedName("Phone") var phone : String,
    @SerializedName("Email") var email : String
) : Serializable