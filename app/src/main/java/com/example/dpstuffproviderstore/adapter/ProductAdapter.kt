package com.example.dpstuffproviderstore.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import androidx.annotation.NonNull
import androidx.cardview.widget.CardView
import androidx.recyclerview.widget.RecyclerView
import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.models.ProductData

internal class ProductAdapter(private var productsList: List<ProductData>) : RecyclerView.Adapter<ProductAdapter.MyViewHolder>() {
    internal class MyViewHolder(view: View) : RecyclerView.ViewHolder(view){
        val productImage: ImageView = view.findViewById(R.id.imageProduct)
        val productPrice: TextView = view.findViewById(R.id.tvPrice)
        val productTitle: TextView = view.findViewById(R.id.tvTitleProduct)
        val productStore: TextView = view.findViewById(R.id.tvStoreProduct)
        val productRating: TextView = view.findViewById(R.id.tvRatingProduct)
        val btnCart: Button = view.findViewById(R.id.btnAddCart)
    }

    @NonNull
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MyViewHolder {
        val itemView = LayoutInflater.from(parent.context)
                .inflate(R.layout.item_product, parent, false)
        return MyViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: ProductAdapter.MyViewHolder, position: Int) {
        holder.productPrice.text = "${productsList[position].price} ₽"
        holder.productTitle.text = productsList[position].name
        holder.productStore.text = "Магазин: ${productsList[position].store}"
        holder.productRating.text = "Рейтинг: ${productsList[position].rating}/5"

        holder.btnCart.setOnClickListener {
            Toast.makeText(holder.itemView.context, "${productsList[position].name} Добавлен в корзину", Toast.LENGTH_LONG).show()
        }
    }

    override fun getItemCount(): Int {
        return productsList.size
    }
}