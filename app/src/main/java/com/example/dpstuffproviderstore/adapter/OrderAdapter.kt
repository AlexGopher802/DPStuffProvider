package com.example.dpstuffproviderstore.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.ImageView
import android.widget.TextView
import androidx.annotation.NonNull
import androidx.recyclerview.widget.RecyclerView
import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.fragment.CartFragment
import com.example.dpstuffproviderstore.fragment.OrdersFragment
import com.example.dpstuffproviderstore.models.ClientData
import com.example.dpstuffproviderstore.models.OrderData
import com.example.dpstuffproviderstore.models.ProductData
import com.example.dpstuffproviderstore.other.ClientApiService

internal class OrderAdapter(private var ordersList: List<OrderData>, private var fragment: OrdersFragment) : RecyclerView.Adapter<OrderAdapter.MyViewHolder>() {
    internal class MyViewHolder(view: View) : RecyclerView.ViewHolder(view){
        val orderInfo: TextView = view.findViewById(R.id.tvOrderInfo)
        val orderDate: TextView = view.findViewById(R.id.tvOrderDate)
        val orderStatus: TextView = view.findViewById(R.id.tvOrderStatus)
        val orderNum: TextView = view.findViewById(R.id.tvOrderNum)
    }

    @NonNull
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): OrderAdapter.MyViewHolder {
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_order, parent, false)
        return OrderAdapter.MyViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: OrderAdapter.MyViewHolder, position: Int) {
        holder.orderNum.text = "Заказ №${ordersList[position].id}"
        holder.orderDate.text = "${ordersList[position].deliveryDate}"
        holder.orderStatus.text = "${ordersList[position].status}"
        holder.orderInfo.text = "Итого: ${ordersList[position].summ} ₽"
    }

    override fun getItemCount(): Int {
        return ordersList.size
    }
}