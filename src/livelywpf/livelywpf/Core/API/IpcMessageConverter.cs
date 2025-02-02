﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace livelywpf.Core.API
{
    class IpcMessageConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IpcMessage));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            return (MessageType)jo["Type"].Value<int>() switch
            {
                MessageType.cmd_reload => jo.ToObject<LivelyReloadCmd>(serializer),
                MessageType.cmd_close => jo.ToObject<LivelyCloseCmd>(serializer),
                MessageType.lsp_perfcntr => jo.ToObject<LivelySystemInformation>(serializer),
                MessageType.lsp_nowplaying => jo.ToObject<LivelySystemNowPlaying>(serializer),
                MessageType.lp_slider => jo.ToObject<LivelySlider>(serializer),
                MessageType.lp_textbox => jo.ToObject<LivelyTextBox>(serializer),
                MessageType.lp_dropdown => jo.ToObject<LivelyDropdown>(serializer),
                MessageType.lp_fdropdown => jo.ToObject<LivelyFolderDropdown>(serializer),
                MessageType.lp_button => jo.ToObject<LivelyButton>(serializer),
                MessageType.lp_cpicker => jo.ToObject<LivelyColorPicker>(serializer),
                MessageType.lp_chekbox => jo.ToObject<LivelyCheckbox>(serializer),
                _ => null,
             };
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
