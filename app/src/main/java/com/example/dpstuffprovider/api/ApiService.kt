package com.example.dpstuffprovider.api

import android.util.Log
import com.example.dpstuffprovider.interfaces.ICouriers
import com.example.dpstuffprovider.interfaces.IOrders
import com.example.dpstuffprovider.models.ClientData
import com.example.dpstuffprovider.models.CouriersData
import com.example.dpstuffprovider.models.OrderComposData
import com.example.dpstuffprovider.models.OrdersData
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import kotlin.math.log

class ApiService {
    fun getOrders(onResult: (List<OrdersData>?) -> Unit){
        val api = ApiBuilder.buildService(IOrders::class.java)

        api.GetOrders().enqueue(object : Callback<List<OrdersData>> {
            override fun onResponse(call: Call<List<OrdersData>>,
                                    response: Response<List<OrdersData>>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!!)
                }
                else{
                    Log.i("myLog", "${response.code()}; ${response.errorBody()}; ${response.message()};")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<OrdersData>>, t: Throwable){
                Log.i("myLog", "Api Failure")
                onResult(null)
            }
        })
    }

    fun getOrdersByStatus(statusName: String, onResult: (List<OrdersData>?) -> Unit){
        val api = ApiBuilder.buildService(IOrders::class.java)

        api.GetOrdersByStatus(statusName).enqueue(object : Callback<List<OrdersData>> {
            override fun onResponse(call: Call<List<OrdersData>>,
                                    response: Response<List<OrdersData>>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!!)
                }
                else{
                    Log.i("myLog", "${response.code()}; ${response.errorBody()}; ${response.message()};")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<OrdersData>>, t: Throwable){
                Log.i("myLog", "Api Failure")
                onResult(null)
            }
        })
    }

    fun getOrderCompos(id: Int, onResult: (List<OrderComposData>?) -> Unit){
        val api = ApiBuilder.buildService(IOrders::class.java)

        api.GetOrderCompos(id).enqueue(object : Callback<List<OrderComposData>> {
            override fun onResponse(call: Call<List<OrderComposData>>,
                                    response: Response<List<OrderComposData>>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!!)
                }
                else{
                    Log.i("myLog", "${response.code()}; ${response.errorBody()}; ${response.message()};")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<OrderComposData>>, t: Throwable){
                Log.i("myLog", "Api Failure")
                onResult(null)
            }
        })
    }

    fun getClient(id: Int, onResult: (List<ClientData>?) -> Unit){
        val api = ApiBuilder.buildService(IOrders::class.java)

        api.GetClient(id).enqueue(object : Callback<List<ClientData>> {
            override fun onResponse(call: Call<List<ClientData>>,
                                    response: Response<List<ClientData>>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!!)
                }
                else{
                    Log.i("myLog", "${response.code()}; ${response.errorBody()}; ${response.message()};")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<ClientData>>, t: Throwable){
                Log.i("myLog", "Api Failure")
                onResult(null)
            }
        })
    }

    fun getCouriers(login: String, password: String, onResult: (List<CouriersData>?) -> Unit){
        val api = ApiBuilder.buildService(ICouriers::class.java)

        api.GetCouriers(login, password).enqueue(object : Callback<List<CouriersData>> {
            override fun onResponse(call: Call<List<CouriersData>>,
                                    response: Response<List<CouriersData>>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!!)
                }
                else{
                    Log.i("myLog", "${response.code()}; ${response.errorBody()}; ${response.message()};")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<CouriersData>>, t: Throwable){
                Log.i("myLog", "Api Failure")
                onResult(null)
            }
        })
    }
}