package com.example.dpstuffproviderstore.`interface`

import com.example.dpstuffproviderstore.models.ProductData
import retrofit2.Call
import retrofit2.http.GET
import retrofit2.http.Path

interface IProduct {
    @GET("product/{categoryName}/")
    fun GetProductsByCategory(@Path("categoryName") categoryName: String): Call<List<ProductData>>

    @GET("product/")
    fun GetAllProducts(): Call<List<ProductData>>

    @GET("product/GetByName/{productName}/")
    fun GetProductsByName(@Path("productName") productName: String): Call<List<ProductData>>
}