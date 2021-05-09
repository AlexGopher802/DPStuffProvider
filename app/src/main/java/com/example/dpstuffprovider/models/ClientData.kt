package com.example.dpstuffprovider.models

import com.google.gson.annotations.SerializedName
import java.io.Serializable

data class ClientData (
    @SerializedName("id") var id : Int,
    @SerializedName("lastName") var lastName : String,
    @SerializedName("firstName") var firstName : String,
    @SerializedName("patronymic") var patronymic : String,
    @SerializedName("phone") var phone : String,
    @SerializedName("email") var email : String
) : Serializable