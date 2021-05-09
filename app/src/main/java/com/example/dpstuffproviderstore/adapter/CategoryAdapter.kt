package com.example.dpstuffproviderstore.adapter

import android.app.Activity
import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.text.TextUtils.replace
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.annotation.NonNull
import androidx.cardview.widget.CardView
import androidx.core.content.ContextCompat
import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentActivity
import androidx.fragment.app.FragmentManager
import androidx.recyclerview.widget.RecyclerView
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.`interface`.ICategory
import com.example.dpstuffproviderstore.fragment.CatalogFragment
import com.example.dpstuffproviderstore.fragment.HomeFragment
import com.example.dpstuffproviderstore.fragment.ProductsFragment
import com.example.dpstuffproviderstore.models.CategoryData
import com.google.android.material.internal.ContextUtils.getActivity
import com.squareup.picasso.Picasso
import kotlinx.android.synthetic.main.activity_main.*
import kotlinx.android.synthetic.main.fragment_catalog.*
import kotlinx.android.synthetic.main.fragment_catalog.view.*
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import kotlinx.android.synthetic.main.activity_main.*

internal class CategoryAdapter(private var categoryList: List<CategoryData>, private var fragment: CatalogFragment) : RecyclerView.Adapter<CategoryAdapter.MyViewHolder>(){
    internal class MyViewHolder(view: View) : RecyclerView.ViewHolder(view){
        val title : TextView = view.findViewById(R.id.tvTitleCategory)
        val cardCategory : CardView = view.findViewById(R.id.cardCategory)
    }

    @NonNull
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MyViewHolder {
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_category, parent, false)
        return MyViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: MyViewHolder, position: Int) {
        holder.title.text = categoryList[position].name


        holder.cardCategory.setOnClickListener {

            Toast.makeText(holder.itemView.context, "${categoryList[position].name}", Toast.LENGTH_LONG).show()

            val retrofit = Retrofit.Builder()
                .baseUrl("https://dpspapiv220210407004655.azurewebsites.net/api/")
                .addConverterFactory(GsonConverterFactory.create())
                .build()
            val api = retrofit.create(ICategory::class.java)

            api.GetChildCategories(categoryList[position].name).enqueue(object : Callback<List<CategoryData>> {
                override fun onResponse(call: Call<List<CategoryData>>,
                                        response: Response<List<CategoryData>>
                ) {
                    if(response.code() == 200){
                        fragment.inflate!!.recyclerCategory.adapter = CategoryAdapter(response.body()!!, fragment)
                        fragment.inflate!!.titleCategory.visibility = View.VISIBLE
                        fragment.inflate!!.titleCategory.text = response.body()!![0].parentName
                        fragment.inflate!!.linkToCategory.visibility = View.VISIBLE
                        fragment.inflate!!.categoryImg.visibility = View.VISIBLE

                        if(!categoryList[position].imageUrl.isEmpty()){
                            Picasso.with(holder.itemView.context)
                                    .load(categoryList[position].imageUrl)
                                    .placeholder(R.drawable.img_placeholder)
                                    .error(R.drawable.img_placeholder)
                                    .into(fragment.inflate!!.categoryImg)
                        }
                        else{
                            fragment.inflate!!.categoryImg.setImageResource(R.drawable.img_placeholder)
                        }

                        fragment.inflate!!.btnUndo.visibility = View.VISIBLE
                        fragment.inflate!!.btnUndo.setOnClickListener {
                            fragment.inflate!!.recyclerCategory.adapter = CategoryAdapter(categoryList, fragment)
                        }
                    }
                    else{
                        val mainActivity = fragment.activity as MainActivity
                        mainActivity.makeCurrentFragment(ProductsFragment())
                    }
                }

                override fun onFailure(call: Call<List<CategoryData>>, t: Throwable){
                    Toast.makeText(holder.itemView.context, t.message, Toast.LENGTH_LONG).show()
                }
            })
        }
    }

    override fun getItemCount(): Int {
        return categoryList.size
    }
}