package com.example.dpstuffproviderstore.`interface`

import com.example.dpstuffproviderstore.models.CategoryData
import retrofit2.Call
import retrofit2.http.GET
import retrofit2.http.Path

interface ICategory {
    @GET("category/")
    fun GetCategories(): Call<List<CategoryData>>

    @GET("category/GetMainCategories/")
    fun GetMainCategories(): Call<List<CategoryData>>

    @GET("category/{categoryName}/")
    fun GetChildCategories(@Path("categoryName") categoryName: String): Call<List<CategoryData>>
}