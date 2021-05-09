package com.example.dpstuffprovider.fragment

import android.content.Context
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.FrameLayout
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.dpstuffprovider.IOrders
import com.example.dpstuffprovider.MainMenu
import com.example.dpstuffprovider.OrdersAdapter
import com.example.dpstuffprovider.R
import com.example.dpstuffprovider.models.OrdersData
import kotlinx.android.synthetic.main.fragment_all_delivery.*
import kotlinx.android.synthetic.main.fragment_all_delivery.view.*
import kotlinx.coroutines.*
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import kotlin.coroutines.CoroutineContext

class AllDeliveryFragment : Fragment() {

    val retrofit = Retrofit.Builder()
        .baseUrl("https://dpspapiv220210407004655.azurewebsites.net/api/")
        .addConverterFactory(GsonConverterFactory.create())
        .build()
    val api = retrofit.create(IOrders::class.java)

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        val inflate : FrameLayout = inflater.inflate(R.layout.fragment_all_delivery, container, false) as FrameLayout

        GlobalScope.launch {
            inflate.recyclerOrders.layoutManager = LinearLayoutManager(context!!)

            api.GetOders().enqueue(object : Callback<List<OrdersData>> {
                override fun onResponse(call: Call<List<OrdersData>>,
                                        response: Response<List<OrdersData>>
                ) {
                    if(response.code() == 200){
                        inflate.recyclerOrders.adapter = OrdersAdapter(response.body()!!)
                    }
                    else{
                        Toast.makeText(context!!, "Error-code: ${response.code()}", Toast.LENGTH_LONG).show()
                    }
                }

                override fun onFailure(call: Call<List<OrdersData>>, t: Throwable){
                    Toast.makeText(context!!, t.message, Toast.LENGTH_LONG).show()
                }
            })
        }

        return inflate;
    }
}