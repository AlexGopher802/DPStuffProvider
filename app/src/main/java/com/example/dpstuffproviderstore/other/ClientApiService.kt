package com.example.dpstuffproviderstore.other

import android.util.Log
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.`interface`.IClient
import com.example.dpstuffproviderstore.`interface`.IOrder
import com.example.dpstuffproviderstore.adapter.ProductAdapter
import com.example.dpstuffproviderstore.fragment.ErrorFragment
import com.example.dpstuffproviderstore.models.ClientData
import com.example.dpstuffproviderstore.models.OrderData
import com.example.dpstuffproviderstore.models.ProductData
import kotlinx.android.synthetic.main.fragment_home.view.*
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class ClientApiService {
    fun getClient(login: String, password: String, onResult: (ClientData?) -> Unit){
        val api = ServiceBuilder.buildService(IClient::class.java)

        api.GetClient(login, password).enqueue(object : Callback<List<ClientData>> {
            override fun onResponse(call: Call<List<ClientData>>,
                                    response: Response<List<ClientData>>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!![0])
                }
                else{
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<ClientData>>, t: Throwable){
                onResult(null)
            }
        })
    }

    fun addClient(clientData: ClientData, noHashPassword: String, onResult: (ClientData?) -> Unit){
        val api = ServiceBuilder.buildService(IClient::class.java)

        api.addUser(clientData, noHashPassword).enqueue(object : Callback<List<ClientData>> {
            override fun onResponse(call: Call<List<ClientData>>,
                                    response: Response<List<ClientData>>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!![0])
                }
                else{
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<ClientData>>, t: Throwable){
                onResult(null)
            }
        })
    }

    fun addOrder(orderData: OrderData, onResult: (OrderData?) -> Unit){
        val api = ServiceBuilder.buildService(IOrder::class.java)

        api.addOrder(orderData).enqueue(object : Callback<OrderData> {
            override fun onResponse(call: Call<OrderData>,
                                    response: Response<OrderData>
            ) {
                if(response.code() == 200){
                    Log.i("myLog", "код 200, заебумба")
                    onResult(response.body()!!)
                }
                else{
                    Log.i("myLog", "${response.code()};;; ${response.errorBody()};;; ${response.message()};;;")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<OrderData>, t: Throwable){
                Log.i("myLog", "пиздык чирик")
                onResult(null)
            }
        })
    }

    fun getOrderByClient(login: String, password: String, onResult: (List<OrderData>?) -> Unit){
        val api = ServiceBuilder.buildService(IOrder::class.java)

        api.getOrdersByClient(login, password).enqueue(object : Callback<List<OrderData>> {
            override fun onResponse(call: Call<List<OrderData>>,
                                    response: Response<List<OrderData>>
            ) {
                if(response.code() == 200){
                    Log.i("myLog", "код 200, заебумба")
                    onResult(response.body()!!)
                }
                else{
                    Log.i("myLog", "${response.code()};;; ${response.errorBody()};;; ${response.message()};;;")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<OrderData>>, t: Throwable){
                Log.i("myLog", "пиздык чирик")
                onResult(null)
            }
        })
    }
}