package com.example.dpstuffproviderstore.other

import android.util.Log
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.`interface`.ICategory
import com.example.dpstuffproviderstore.`interface`.IClient
import com.example.dpstuffproviderstore.`interface`.IOrder
import com.example.dpstuffproviderstore.`interface`.IProduct
import com.example.dpstuffproviderstore.adapter.ProductAdapter
import com.example.dpstuffproviderstore.fragment.ErrorFragment
import com.example.dpstuffproviderstore.models.*
import kotlinx.android.synthetic.main.fragment_home.view.*
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

/**
 * Класс, реализующий все методы Api-интерфейсов
 */
class ClientApiService {
    /**
     * Получаем объект клиента по логину и паролю (авторизация)
     */
    fun getClient(login: String, password: String, onResult: (ClientData?) -> Unit){
        val api = ServiceBuilder.buildService(IClient::class.java)

        api.GetClient(login, password).enqueue(object : Callback<List<ClientData>> {
            override fun onResponse(call: Call<List<ClientData>>,
                                    response: Response<List<ClientData>>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!![0])
                }
                else{
                    Log.i("myLog", "${response.code()}; ${response.errorBody()}; ${response.message()};")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<ClientData>>, t: Throwable){
                Log.i("myLog", "Api Failure")
                onResult(null)
            }
        })
    }

    /**
     * Регистрация нового пользователя
     */
    fun addClient(clientData: ClientData, noHashPassword: String, onResult: (ClientData?) -> Unit){
        val api = ServiceBuilder.buildService(IClient::class.java)

        api.addUser(clientData, noHashPassword).enqueue(object : Callback<List<ClientData>> {
            override fun onResponse(call: Call<List<ClientData>>,
                                    response: Response<List<ClientData>>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!![0])
                }
                else{
                    Log.i("myLog", "${response.code()}; ${response.errorBody()}; ${response.message()};")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<ClientData>>, t: Throwable){
                Log.i("myLog", "Api Failure")
                onResult(null)
            }
        })
    }

    /**
     * Регистрация нового заказа
     */
    fun addOrder(orderData: OrderData, onResult: (OrderData?) -> Unit){
        val api = ServiceBuilder.buildService(IOrder::class.java)

        api.addOrder(orderData).enqueue(object : Callback<OrderData> {
            override fun onResponse(call: Call<OrderData>,
                                    response: Response<OrderData>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!!)
                }
                else{
                    Log.i("myLog", "${response.code()}; ${response.errorBody()}; ${response.message()};")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<OrderData>, t: Throwable){
                Log.i("myLog", "Api Failure")
                onResult(null)
            }
        })
    }

    /**
     * Получаем список заказов конкретного клиента (по логину и паролю)
     */
    fun getOrderByClient(login: String, password: String, onResult: (List<OrderData>?) -> Unit){
        val api = ServiceBuilder.buildService(IOrder::class.java)

        api.getOrdersByClient(login, password).enqueue(object : Callback<List<OrderData>> {
            override fun onResponse(call: Call<List<OrderData>>,
                                    response: Response<List<OrderData>>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!!)
                }
                else{
                    Log.i("myLog", "${response.code()}; ${response.errorBody()}; ${response.message()};")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<OrderData>>, t: Throwable){
                Log.i("myLog", "Api Failure")
                onResult(null)
            }
        })
    }

    /**
     * Получаем первый объект изображения, относящийся к товару (по id товара)
     */
    fun getImages(productId: Int, onResult: (List<ProductImagesData>?) -> Unit){
        val api = ServiceBuilder.buildService(IProduct::class.java)

        api.GetImages(productId).enqueue(object : Callback<List<ProductImagesData>> {
            override fun onResponse(call: Call<List<ProductImagesData>>,
                                    response: Response<List<ProductImagesData>>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!!)
                }
                else{
                    Log.i("myLog", "${response.code()}; ${response.errorBody()}; ${response.message()};")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<ProductImagesData>>, t: Throwable){
                Log.i("myLog", "Api Failure")
                onResult(null)
            }
        })
    }

    /**
     * Получаем список основных категорий (у которыйх нет родительской категории)
     */
    fun getMainCategories(onResult: (List<CategoryData>?) -> Unit){
        val api = ServiceBuilder.buildService(ICategory::class.java)

        api.GetMainCategories().enqueue(object : Callback<List<CategoryData>> {
            override fun onResponse(call: Call<List<CategoryData>>,
                                    response: Response<List<CategoryData>>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!!)
                }
                else{
                    Log.i("myLog", "${response.code()}; ${response.errorBody()}; ${response.message()};")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<CategoryData>>, t: Throwable){
                Log.i("myLog", "Api Failure")
                onResult(null)
            }
        })
    }

    /**
     * Получаем список дочерних категорий (по имени родительской категории)
     */
    fun getChildCategories(categoryName: String, onResult: (List<CategoryData>?) -> Unit){
        val api = ServiceBuilder.buildService(ICategory::class.java)

        api.GetChildCategories(categoryName).enqueue(object : Callback<List<CategoryData>> {
            override fun onResponse(call: Call<List<CategoryData>>,
                                    response: Response<List<CategoryData>>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!!)
                }
                else{
                    Log.i("myLog", "${response.code()}; ${response.errorBody()}; ${response.message()};")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<CategoryData>>, t: Throwable){
                Log.i("myLog", "Api Failure")
                onResult(null)
            }
        })
    }

    /**
     * Получаем список всех товаров в БД
     */
    fun getAllProducts(onResult: (List<ProductData>?) -> Unit){
        val api = ServiceBuilder.buildService(IProduct::class.java)

        api.GetAllProducts().enqueue(object : Callback<List<ProductData>> {
            override fun onResponse(call: Call<List<ProductData>>,
                                    response: Response<List<ProductData>>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!!)
                }
                else{
                    Log.i("myLog", "${response.code()}; ${response.errorBody()}; ${response.message()};")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<ProductData>>, t: Throwable){
                Log.i("myLog", "Api Failure")
                onResult(null)
            }
        })
    }

    /**
     * Получаем список товаров (по наименованию)
     */
    fun getProductsByName(productName: String, onResult: (List<ProductData>?) -> Unit){
        val api = ServiceBuilder.buildService(IProduct::class.java)

        api.GetProductsByName(productName).enqueue(object : Callback<List<ProductData>> {
            override fun onResponse(call: Call<List<ProductData>>,
                                    response: Response<List<ProductData>>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!!)
                }
                else{
                    Log.i("myLog", "${response.code()}; ${response.errorBody()}; ${response.message()};")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<ProductData>>, t: Throwable){
                Log.i("myLog", "Api Failure")
                onResult(null)
            }
        })
    }

    /**
     * Получаем список товаров, относящихся к категории и ко всем дочерним категориям (по имени категории)
     */
    fun getProductsByCategory(categoryName: String, onResult: (List<ProductData>?) -> Unit){
        val api = ServiceBuilder.buildService(IProduct::class.java)

        api.GetProductsByCategory(categoryName).enqueue(object : Callback<List<ProductData>> {
            override fun onResponse(call: Call<List<ProductData>>,
                                    response: Response<List<ProductData>>
            ) {
                if(response.code() == 200){
                    onResult(response.body()!!)
                }
                else{
                    Log.i("myLog", "${response.code()}; ${response.errorBody()}; ${response.message()};")
                    onResult(null)
                }
            }

            override fun onFailure(call: Call<List<ProductData>>, t: Throwable){
                Log.i("myLog", "Api Failure")
                onResult(null)
            }
        })
    }
}