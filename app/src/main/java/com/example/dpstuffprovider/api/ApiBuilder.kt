package com.example.dpstuffprovider.api

import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

/**
 * Билдер retrofit с корневой ссылкой Api
 */
object ApiBuilder {
    fun<T> buildService(service: Class<T>): T{
        val retrofit = Retrofit.Builder()
                .baseUrl("http://194.32.248.98:49155/api/")
                .addConverterFactory(GsonConverterFactory.create())
                .build()
        val api = retrofit.create(service)

        return api
    }
}