package com.example.dpstuffproviderstore.fragment

import android.opengl.Visibility
import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.`interface`.ICategory
import com.example.dpstuffproviderstore.adapter.CategoryAdapter
import com.example.dpstuffproviderstore.models.CategoryData
import kotlinx.android.synthetic.main.fragment_catalog.*
import kotlinx.android.synthetic.main.fragment_catalog.view.*
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class CatalogFragment() : Fragment() {

    var categoryList : List<CategoryData> = listOf()
    var inflate : View? = null

    val retrofit = Retrofit.Builder()
        .baseUrl("https://dpspapiv220210407004655.azurewebsites.net/api/")
        .addConverterFactory(GsonConverterFactory.create())
        .build()
    val api = retrofit.create(ICategory::class.java)

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        inflate = inflater.inflate(R.layout.fragment_catalog, container, false)

        GlobalScope.launch {
            inflate!!.recyclerCategory.layoutManager = LinearLayoutManager(context!!)

            api.GetMainCategories().enqueue(object : Callback<List<CategoryData>> {
                override fun onResponse(call: Call<List<CategoryData>>,
                                        response: Response<List<CategoryData>>
                ) {
                    if(response.code() == 200){
                        inflate!!.recyclerCategory.adapter = CategoryAdapter(response.body()!!, this@CatalogFragment)
                    }
                    else{
                        Toast.makeText(context!!, "Error-code: ${response.code()}", Toast.LENGTH_LONG).show()
                    }
                }

                override fun onFailure(call: Call<List<CategoryData>>, t: Throwable){
                    Toast.makeText(context!!, t.message, Toast.LENGTH_LONG).show()
                }
            })

        }

        return inflate
    }
}