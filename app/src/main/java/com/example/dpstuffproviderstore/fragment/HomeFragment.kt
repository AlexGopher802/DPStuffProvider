package com.example.dpstuffproviderstore.fragment

import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.R.string
import com.example.dpstuffproviderstore.`interface`.IProduct
import com.example.dpstuffproviderstore.adapter.ProductAdapter
import com.example.dpstuffproviderstore.models.ProductData
import kotlinx.android.synthetic.main.fragment_home.view.*
import kotlinx.android.synthetic.main.fragment_products.view.*
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class HomeFragment : Fragment() {

    val retrofit = Retrofit.Builder()
            .baseUrl("https://dpspapiv220210407004655.azurewebsites.net/api/")
            .addConverterFactory(GsonConverterFactory.create())
            .build()
    val api = retrofit.create(IProduct::class.java)

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        val inflate = inflater.inflate(R.layout.fragment_home, container, false)
        val mainActivity = activity as MainActivity

        inflate.recyclerBestProducts.layoutManager = LinearLayoutManager(context!!)

        api.GetAllProducts().enqueue(object : Callback<List<ProductData>> {
            override fun onResponse(call: Call<List<ProductData>>,
                                    response: Response<List<ProductData>>
            ) {
                if(response.code() == 200){
                    inflate.recyclerBestProducts.adapter = ProductAdapter(response.body()!!, this@HomeFragment)
                    //mainActivity.cartList.add(response.body()!![0])
                }
                else{
                    val fragment = ErrorFragment()
                    fragment.statusCode = response.code().toString()
                    mainActivity.makeCurrentFragment(fragment)
                }
            }

            override fun onFailure(call: Call<List<ProductData>>, t: Throwable){
                val fragment = ErrorFragment()
                mainActivity.makeCurrentFragment(fragment)
            }
        })

        return inflate
    }

}