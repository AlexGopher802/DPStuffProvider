package com.example.dpstuffprovider

import com.example.dpstuffprovider.models.CouriersData
import retrofit2.Call
import retrofit2.http.GET
import retrofit2.http.Path

interface ICouriers {
    @GET("couriers/{login}/{password}")
    fun GetCouriers(@Path("login") log : String, @Path("password") pass : String): Call<List<CouriersData>>
}