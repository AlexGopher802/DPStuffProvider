package com.example.dpstuffprovider.interfaces

import com.example.dpstuffprovider.models.CouriersData
import com.example.dpstuffprovider.models.OrdersData
import retrofit2.Call
import retrofit2.http.*

interface ICouriers {
    @GET("couriers/{login}/{password}")
    fun GetCouriers(@Path("login") log : String, @Path("password") pass : String): Call<List<CouriersData>>

    @Headers("Content-Type: application/json")
    @POST("couriers/UpdateOrdersQuantity")
    fun UpdateOrdersQuantity(@Body couriersData: CouriersData): Call<CouriersData>
}