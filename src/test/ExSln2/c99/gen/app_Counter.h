// finlang generated file for c# app.Counter class
#pragma once

#include <stdint.h>



/// <summary>
/// A simple counter class.
/// </summary>
typedef struct app_Counter app_Counter;
struct app_Counter
{
    /// <summary>
    /// The counts array.
    /// </summary>
    uint8_t * _counts;

    /// <summary>
    /// Another couple of fields.
    /// </summary>
    uint8_t _count_length, _set_invocations_counts;
};


    void app_Counter_ctor(app_Counter * self, uint8_t * counts, uint8_t count_length);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    uint8_t app_Counter_get(app_Counter * self, uint8_t index);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    void app_Counter_set(app_Counter * self, uint8_t index, uint8_t value);
