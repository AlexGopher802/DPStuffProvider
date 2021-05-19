package com.example.dpstuffproviderstore.fragment

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.`interface`.ICategory
import com.example.dpstuffproviderstore.`interface`.IProduct
import com.example.dpstuffproviderstore.adapter.CategoryAdapter
import com.example.dpstuffproviderstore.adapter.ProductAdapter
import com.example.dpstuffproviderstore.models.CategoryData
import com.example.dpstuffproviderstore.models.ProductData
import kotlinx.android.synthetic.main.fragment_catalog.view.*
import kotlinx.android.synthetic.main.fragment_products.view.*
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class ProductsFragment : Fragment() {

    var inflate : View? = null

    val retrofit = Retrofit.Builder()
            .baseUrl("https://dpspapiv220210407004655.azurewebsites.net/api/")
            .addConverterFactory(GsonConverterFactory.create())
            .build()
    val api = retrofit.create(IProduct::class.java)

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?,
                              savedInstanceState: Bundle?): View? {
        val inflate = inflater.inflate(R.layout.fragment_products, container, false)

        inflate.recyclerProducts.layoutManager = LinearLayoutManager(context!!)

        val mainActivity = activity as MainActivity

        if(mainActivity.modeSearch == MainActivity.EnumModeSearch.CATEGORY)
        {
            api.GetProductsByCategory(mainActivity.productCategory).enqueue(object : Callback<List<ProductData>> {
                override fun onResponse(call: Call<List<ProductData>>,
                                        response: Response<List<ProductData>>
                ) {
                    if(response.code() == 200){
                        inflate.recyclerProducts.adapter = ProductAdapter(response.body()!!, this@ProductsFragment)
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
        }

        if(mainActivity.modeSearch == MainActivity.EnumModeSearch.NAME)
        {
            api.GetProductsByName(mainActivity.productName).enqueue(object : Callback<List<ProductData>> {
                override fun onResponse(call: Call<List<ProductData>>,
                                        response: Response<List<ProductData>>
                ) {
                    if(response.code() == 200){
                        inflate.recyclerProducts.adapter = ProductAdapter(response.body()!!, this@ProductsFragment)
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
        }



        return inflate
    }

}