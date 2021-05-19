package com.example.dpstuffproviderstore.`interface`

import com.example.dpstuffproviderstore.models.ClientData
import com.example.dpstuffproviderstore.models.ProductData
import retrofit2.Call
import retrofit2.http.*

interface IClient {
    @GET("clients/{login}/{password}")
    fun GetClient(@Path("login") login: String, @Path("password") password: String): Call<List<ClientData>>

    @Headers("Content-Type: application/json")
    @POST("clients/regclient")
    fun addUser(@Body clientData: ClientData, @Query("noHashPassword") noHashPassword: String): Call<List<ClientData>>
}