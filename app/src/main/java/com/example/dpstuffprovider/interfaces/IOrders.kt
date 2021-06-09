package com.example.dpstuffprovider.interfaces

import com.example.dpstuffprovider.models.ClientData
import com.example.dpstuffprovider.models.OrderComposData
import com.example.dpstuffprovider.models.OrdersData
import retrofit2.Call
import retrofit2.http.*

interface IOrders {
    @GET("orders")
    fun GetOrders(): Call<List<OrdersData>>

    @GET("orders/getorders/{id}")
    fun GetOrderCompos(@Path("id") id : Int): Call<List<OrderComposData>>

    @GET("orders/getclient/{id}")
    fun GetClient(@Path("id") id : Int): Call<List<ClientData>>

    @GET("orders/GetOrdersByStatus/{statusName}")
    fun GetOrdersByStatus(@Path("statusName") statusName : String): Call<List<OrdersData>>

    @GET("orders/GetActiveOrdersByCourier/{idCourier}")
    fun GetActiveOrdersByCourier(@Path("idCourier") idCourier : Int): Call<List<OrdersData>>

    @Headers("Content-Type: application/json")
    @POST("orders/UpdateStatus")
    fun UpdateStatus(@Body ordersData: OrdersData): Call<OrdersData>
}