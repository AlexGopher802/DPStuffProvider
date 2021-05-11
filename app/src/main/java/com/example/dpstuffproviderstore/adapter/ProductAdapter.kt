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
import com.example.dpstuffproviderstore.`interface`.ICategory
import com.example.dpstuffproviderstore.`interface`.IProduct
import com.example.dpstuffproviderstore.fragment.ProductsFragment
import com.example.dpstuffproviderstore.models.CategoryData
import com.example.dpstuffproviderstore.models.ProductData
import com.example.dpstuffproviderstore.models.ProductImagesData
import com.squareup.picasso.Picasso
import kotlinx.android.synthetic.main.fragment_catalog.view.*
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

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

        val retrofit = Retrofit.Builder()
                .baseUrl("https://dpspapiv220210407004655.azurewebsites.net/api/")
                .addConverterFactory(GsonConverterFactory.create())
                .build()
        val api = retrofit.create(IProduct::class.java)

        api.GetImages(productsList[position].id).enqueue(object : Callback<List<ProductImagesData>> {
            override fun onResponse(call: Call<List<ProductImagesData>>,
                                    response: Response<List<ProductImagesData>>
            ) {
                if(response.code() == 200){
                    Picasso.with(holder.itemView.context)
                            .load(response.body()!![0].imageUrl)
                            .placeholder(R.drawable.img_placeholder)
                            .error(R.drawable.img_placeholder)
                            .into(holder.productImage)
                }
                else{
                    Toast.makeText(holder.itemView.context, "Error-code: ${response.code()}", Toast.LENGTH_LONG).show()
                }
            }

            override fun onFailure(call: Call<List<ProductImagesData>>, t: Throwable){
                Toast.makeText(holder.itemView.context, t.message, Toast.LENGTH_LONG).show()
            }
        })
    }

    override fun getItemCount(): Int {
        return productsList.size
    }
}