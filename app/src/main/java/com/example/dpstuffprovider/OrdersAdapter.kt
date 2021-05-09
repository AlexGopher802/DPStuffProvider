package com.example.dpstuffprovider

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.annotation.NonNull
import androidx.core.content.ContextCompat.startActivity
import androidx.recyclerview.widget.RecyclerView
import com.example.dpstuffprovider.models.ClientData
import com.example.dpstuffprovider.models.OrderCompos
import com.example.dpstuffprovider.models.OrderComposData
import com.example.dpstuffprovider.models.OrdersData
import kotlinx.android.synthetic.main.activity_main_menu.*
import retrofit2.*
import retrofit2.converter.gson.GsonConverterFactory

internal class OrdersAdapter (private var ordersList: List<OrdersData>) : RecyclerView.Adapter<OrdersAdapter.MyViewHolder>(){
    internal class MyViewHolder(view: View) : RecyclerView.ViewHolder(view){
        val address : TextView = view.findViewById(R.id.tvAddress)
        val date : TextView = view.findViewById(R.id.tvDate)
        val timeFrom : TextView = view.findViewById(R.id.tvTimeFrom)
        val timeTo : TextView = view.findViewById(R.id.tvTimeTo)
        val moreInfo : Button = view.findViewById(R.id.btnMoreInfo)
        //val orderInfo : OrdersData =
    }

    @NonNull
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MyViewHolder {
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.order_item, parent, false)
        return MyViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: MyViewHolder, position: Int) {
        Log.e("", "onBindViewHolder started")
        holder.address.text = ordersList[position].address
        holder.date.text = ordersList[position].deliveryDate
        holder.timeFrom.text = ordersList[position].timeFrom
        holder.timeTo.text = ordersList[position].timeTo


        holder.moreInfo.setOnClickListener{

            val intent : Intent = Intent(holder.itemView.context, MoreInfo::class.java)

            val retrofit = Retrofit.Builder()
                .baseUrl("https://dpspapiv220210407004655.azurewebsites.net/api/")
                .addConverterFactory(GsonConverterFactory.create())
                .build()
            val api = retrofit.create(IOrders::class.java)

            api.GetClient(ordersList[position].id).enqueue(object : Callback<List<ClientData>> {
                override fun onResponse(call: Call<List<ClientData>>,
                                        response: Response<List<ClientData>>) {
                    if(response.code() == 200){
                        intent.putExtra("orderInfo", ordersList[position])
                        intent.putExtra("clientInfo", response.body()!![0])
                        api.GetOderCompos(ordersList[position].id).enqueue(object : Callback<List<OrderComposData>> {
                            override fun onResponse(call: Call<List<OrderComposData>>,
                                                    response: Response<List<OrderComposData>>) {
                                if(response.code() == 200){
                                    
                                    intent.putExtra("orderCompos", OrderCompos(response.body()!!))
                                    startActivity(holder.itemView.context, intent, Bundle.EMPTY)
                                }
                            }

                            override fun onFailure(call: Call<List<OrderComposData>>, t: Throwable){

                            }
                        })
                    }
                }

                override fun onFailure(call: Call<List<ClientData>>, t: Throwable){

                }
            })
        }
    }

    override fun getItemCount(): Int {
        return ordersList.size
    }
}