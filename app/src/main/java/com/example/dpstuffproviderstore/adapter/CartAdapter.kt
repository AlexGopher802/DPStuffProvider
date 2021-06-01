package com.example.dpstuffproviderstore.adapter

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import androidx.annotation.NonNull
import androidx.cardview.widget.CardView
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.RecyclerView
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.`interface`.ICategory
import com.example.dpstuffproviderstore.`interface`.IProduct
import com.example.dpstuffproviderstore.fragment.CartEmptyFragment
import com.example.dpstuffproviderstore.fragment.CartFragment
import com.example.dpstuffproviderstore.fragment.ErrorFragment
import com.example.dpstuffproviderstore.fragment.ProductsFragment
import com.example.dpstuffproviderstore.models.CategoryData
import com.example.dpstuffproviderstore.models.ProductData
import com.example.dpstuffproviderstore.models.ProductImagesData
import com.google.gson.Gson
import com.squareup.picasso.Picasso
import kotlinx.android.synthetic.main.fragment_cart.view.*
import kotlinx.android.synthetic.main.fragment_catalog.view.*
import org.w3c.dom.Text
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

internal class CartAdapter(private var productsList: List<ProductData>, private var fragment: CartFragment) : RecyclerView.Adapter<CartAdapter.MyViewHolder>() {
    internal class MyViewHolder(view: View) : RecyclerView.ViewHolder(view){
        val productImage: ImageView = view.findViewById(R.id.imageProductCart)
        val productPrice: TextView = view.findViewById(R.id.tvPriceCart)
        val productTitle: TextView = view.findViewById(R.id.tvTitleProductCart)
        val productStore: TextView = view.findViewById(R.id.tvStoreProductCart)
        val productRating: TextView = view.findViewById(R.id.tvRatingProductCart)
        val poductQuantity: TextView = view.findViewById(R.id.tvQuantityCart)

        val btnPlus: Button = view.findViewById(R.id.btnPlusCart)
        val btnMinus: Button = view.findViewById(R.id.btnMinusCart)
        val btnTrash: Button = view.findViewById(R.id.btnDeleteCart)
    }

    @NonNull
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MyViewHolder {
        val itemView = LayoutInflater.from(parent.context)
                .inflate(R.layout.item_cart, parent, false)
        return MyViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: CartAdapter.MyViewHolder, position: Int) {
        val mainActivity = fragment.activity as MainActivity

        holder.productPrice.text = "${productsList[position].price} ₽"
        holder.productTitle.text = productsList[position].name
        holder.productStore.text = "Магазин: ${productsList[position].store}"
        holder.productRating.text = "Рейтинг: ${productsList[position].rating}/5"
        holder.poductQuantity.text = "${productsList[position].quantity ?: 1} Шт"

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
                    val fragment = ErrorFragment()
                    fragment.statusCode = response.code().toString()
                    mainActivity.makeCurrentFragment(fragment)
                }
            }

            override fun onFailure(call: Call<List<ProductImagesData>>, t: Throwable){
                val fragment = ErrorFragment()
                mainActivity.makeCurrentFragment(fragment)
            }
        })

        holder.btnPlus.setOnClickListener {
            mainActivity.cartList[position].quantity = (productsList[position].quantity ?: 1) + 1
            val editor = mainActivity.getSharedPreferences("sp", Context.MODE_PRIVATE).edit()
            editor.putString("cartList", Gson().toJson(mainActivity.cartList))
            editor.apply()

            holder.poductQuantity.text = "${mainActivity.cartList[position].quantity ?: 1} Шт"

            var summ : Double = 0.0
            for(i in mainActivity.cartList){
                summ += i.price!! * (i.quantity ?: 1)
            }
            fragment.inflate!!.tvCheckout.text = "Итого: ${summ} ₽"
            fragment.summ = summ
        }

        fun removeProduct(product : ProductData){
            mainActivity.cartList.remove(product)
            val editor = mainActivity.getSharedPreferences("sp", Context.MODE_PRIVATE).edit()


            if(mainActivity.cartList.size == 0){
                mainActivity.makeCurrentFragment(CartEmptyFragment())
                mainActivity.cartFragment = CartEmptyFragment()

                editor.putString("cartList", "").apply()
            }
            else{
                editor.putString("cartList", Gson().toJson(mainActivity.cartList)).apply()

                fragment.inflate!!.recyclerCart.adapter = CartAdapter(mainActivity.cartList, fragment)
                fragment.inflate!!.tvTitle.text = "${mainActivity.cartList.size} Товара:"

                var summ : Double = 0.0
                for(i in mainActivity.cartList){
                    summ += i.price!! * (i.quantity ?: 1)
                }
                fragment.inflate!!.tvCheckout.text = "Итого: ${summ} ₽"
            }
        }

        holder.btnMinus.setOnClickListener {
            mainActivity.cartList[position].quantity = (productsList[position].quantity ?: 1) - 1
            if (mainActivity.cartList[position].quantity!! <= 0){
                removeProduct(productsList[position])
            }
            else{
                val editor = mainActivity.getSharedPreferences("sp", Context.MODE_PRIVATE).edit()
                editor.putString("cartList", Gson().toJson(mainActivity.cartList))
                editor.apply()

                holder.poductQuantity.text = "${mainActivity.cartList[position].quantity ?: 1} Шт"

                var summ : Double = 0.0
                for(i in mainActivity.cartList){
                    summ += i.price!! * (i.quantity ?: 1)
                }
                fragment.inflate!!.tvCheckout.text = "Итого: ${summ} ₽"
                fragment.summ = summ
            }
        }

        holder.btnTrash.setOnClickListener {
            removeProduct(productsList[position])
        }

    }

    override fun getItemCount(): Int {
        return productsList.size
    }
}