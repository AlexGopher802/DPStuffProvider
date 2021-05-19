package com.example.dpstuffproviderstore.other

import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.`interface`.IClient
import com.example.dpstuffproviderstore.adapter.ProductAdapter
import com.example.dpstuffproviderstore.fragment.ErrorFragment
import com.example.dpstuffproviderstore.models.ClientData
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
}