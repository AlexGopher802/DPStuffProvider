package com.example.dpstuffproviderstore.other


import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.`interface`.ICategory
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

/**
 * Билдер retrofit с корневой ссылкой Api
 */
object ServiceBuilder {
    fun<T> buildService(service: Class<T>): T{
        val retrofit = Retrofit.Builder()
                .baseUrl("https://dpspapiv220210407004655.azurewebsites.net/api/")
                .addConverterFactory(GsonConverterFactory.create())
                .build()
        val api = retrofit.create(service)

        return api
    }
}