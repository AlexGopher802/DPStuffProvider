package com.example.dpstuffprovider.adapters

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import androidx.annotation.NonNull
import androidx.core.content.ContextCompat.startActivity
import androidx.recyclerview.widget.RecyclerView
import com.example.dpstuffprovider.MainMenu
import com.example.dpstuffprovider.interfaces.IOrders
import com.example.dpstuffprovider.MoreInfo
import com.example.dpstuffprovider.R
import com.example.dpstuffprovider.api.ApiService
import com.example.dpstuffprovider.models.ClientData
import com.example.dpstuffprovider.models.OrderCompos
import com.example.dpstuffprovider.models.OrderComposData
import com.example.dpstuffprovider.models.OrdersData
import retrofit2.*
import retrofit2.converter.gson.GsonConverterFactory

/**
 * Адаптер для отображения списка заказов
 */
internal class OrdersAdapter (private var ordersList: List<OrdersData>, private var activity: MainMenu) : RecyclerView.Adapter<OrdersAdapter.MyViewHolder>(){
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
        return MyViewHolder(
            itemView
        )
    }

    override fun onBindViewHolder(holder: MyViewHolder, position: Int) {
        Log.e("", "onBindViewHolder started")
        holder.address.text = ordersList[position].address
        holder.date.text = ordersList[position].deliveryDate
        holder.timeFrom.text = "С ${ordersList[position].timeFrom}"
        holder.timeTo.text = "До ${ordersList[position].timeTo}"


        holder.moreInfo.setOnClickListener{

            val intent : Intent = Intent(holder.itemView.context, MoreInfo::class.java)

            ApiService().getClient(ordersList[position].id) {
                if(it != null){
                    intent.putExtra("orderInfo", ordersList[position])
                    intent.putExtra("clientInfo", it[0])

                    ApiService().getOrderCompos(ordersList[position].id) {
                        if(it != null) {
                            intent.putExtra("orderCompos", OrderCompos(it))
                            intent.putExtra("courier", activity.courier)
                            startActivity(holder.itemView.context, intent, Bundle.EMPTY)
                        }
                    }
                }
            }
        }
    }

    override fun getItemCount(): Int {
        return ordersList.size
    }
}