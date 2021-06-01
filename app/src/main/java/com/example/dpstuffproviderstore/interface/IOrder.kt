package com.example.dpstuffproviderstore.`interface`

import com.example.dpstuffproviderstore.models.ClientData
import com.example.dpstuffproviderstore.models.OrderData
import retrofit2.Call
import retrofit2.http.*

interface IOrder {
    @Headers("Content-Type: application/json")
    @POST("orders/RegOrder")
    fun addOrder(@Body orderData: OrderData): Call<OrderData>

    @GET("orders/GetOrders/{login}/{password}")
    fun getOrdersByClient(@Path("login") login: String, @Path("password") password: String): Call<List<OrderData>>
}