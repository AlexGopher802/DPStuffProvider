package com.example.dpstuffproviderstore.fragment

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.example.dpstuffproviderstore.R
import kotlinx.android.synthetic.main.fragment_error.view.*

/**
 * Фрагмент заглушки на случай ошибок (не отвечает api)
 */
class ErrorFragment : Fragment() {

    var statusCode : String = "404"

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?,
                              savedInstanceState: Bundle?): View? {
        val inflate = inflater.inflate(R.layout.fragment_error, container, false)

        inflate.tvStatusCode.text = statusCode

        return inflate
    }

}