package com.example.dpstuffproviderstore.models

import com.google.gson.annotations.SerializedName

data class ClientData (
    @SerializedName("id") var id : Int,
    @SerializedName("lastName") var lastName : String,
    @SerializedName("firstName") var firstName : String,
    @SerializedName("patronymic") var patronymic : String,
    @SerializedName("login") var login : String,
    @SerializedName("password") var password : String,
    @SerializedName("phone") var phone : String,
    @SerializedName("email") var email : String
)