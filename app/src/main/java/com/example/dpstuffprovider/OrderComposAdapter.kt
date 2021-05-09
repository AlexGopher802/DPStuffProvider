package com.example.dpstuffprovider

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import androidx.annotation.NonNull
import androidx.recyclerview.widget.RecyclerView
import com.example.dpstuffprovider.R
import com.example.dpstuffprovider.models.OrderComposData
import org.w3c.dom.Text

internal class OrderComposAdapter(private val orderComposList: List<OrderComposData>) : RecyclerView.Adapter<OrderComposAdapter.MyViewHolder>() {
    internal class MyViewHolder(view: View) : RecyclerView.ViewHolder(view){
        val title : TextView = view.findViewById(R.id.tvTitle)
        val storeName : TextView = view.findViewById(R.id.tvStore)
        val quantity : TextView = view.findViewById(R.id.tvQuantity)
    }

    @NonNull
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MyViewHolder {
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.order_products_item, parent, false)
        return MyViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: MyViewHolder, position: Int) {
        holder.title.text = orderComposList[position].name
        holder.storeName.text = orderComposList[position].shopName
        holder.quantity.text = "${orderComposList[position].quantity} Шт"
    }

    override fun getItemCount(): Int {
        return orderComposList.size
    }
}