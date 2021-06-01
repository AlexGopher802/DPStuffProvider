package com.example.dpstuffproviderstore.fragment

import android.opengl.Visibility
import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.view.inputmethod.EditorInfo
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.`interface`.ICategory
import com.example.dpstuffproviderstore.adapter.CategoryAdapter
import com.example.dpstuffproviderstore.models.CategoryData
import com.example.dpstuffproviderstore.other.ClientApiService
import kotlinx.android.synthetic.main.fragment_catalog.*
import kotlinx.android.synthetic.main.fragment_catalog.view.*
import kotlinx.android.synthetic.main.fragment_error.view.*
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

/**
 * Фрагмент каталога с поиском товаров по наименованию и категориям
 */
class CatalogFragment() : Fragment() {

    var inflate : View? = null

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        inflate = inflater.inflate(R.layout.fragment_catalog, container, false)
        val mainActivity = activity as MainActivity

        GlobalScope.launch {
            inflate!!.recyclerCategory.layoutManager = LinearLayoutManager(context!!)

            ClientApiService().getMainCategories() {

                if(it != null){
                    inflate!!.recyclerCategory.adapter = CategoryAdapter(it, this@CatalogFragment, null)
                }
                else{
                    val fragment = ErrorFragment()
                    fragment.statusCode = "404"
                    mainActivity.makeCurrentFragment(fragment)
                }
            }
        }

        inflate!!.inputTextSearch.setOnEditorActionListener { v, actionId, event ->
            if(actionId == EditorInfo.IME_ACTION_SEARCH){
                Log.i("myLog", "Пошёл поиск...")
                mainActivity.modeSearch = MainActivity.EnumModeSearch.NAME
                mainActivity.productName = inflate!!.inputTextSearch.text.toString()
                mainActivity.makeCurrentFragment(ProductsFragment())
                true
            } else {
                false
            }
        }

        return inflate
    }
}