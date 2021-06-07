package com.example.dpstuffprovider.interfaces

import com.example.dpstuffprovider.models.ClientData
import com.example.dpstuffprovider.models.OrderComposData
import com.example.dpstuffprovider.models.OrdersData
import retrofit2.Call
import retrofit2.http.GET
import retrofit2.http.Path

interface IOrders {
    @GET("orders")
    fun GetOrders(): Call<List<OrdersData>>

    @GET("orders/getorders/{id}")
    fun GetOrderCompos(@Path("id") id : Int): Call<List<OrderComposData>>

    @GET("orders/getclient/{id}")
    fun GetClient(@Path("id") id : Int): Call<List<ClientData>>
}