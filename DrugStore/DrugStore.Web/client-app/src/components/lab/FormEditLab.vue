<template>
  <v-card>
    <v-card-title>
      <span class="headline">Nuevo Laboratorio</span>
    </v-card-title>
    <v-card-text>
      <v-container>
        <v-row>
          <v-col cols="12" sm="6" md="12">
            <v-text-field label="Nombre del Laboratorio*" v-model="labObj.laboratoryName" required></v-text-field>
          </v-col>
              
          <v-col cols="12" sm="6" md="12">
            <v-text-field label="Descripcion*" v-model="labObj.description" required></v-text-field>
          </v-col>
                  
          <v-col cols="12">           
            <v-select
              v-model="labObj.condition"
              :items="[{'estado':'Activo', 'value':true}, {'estado':'Inactivo', 'value':false}]"   
              item-text="estado"
              item-value="value"                       
              label="Estado*"
              required
            ></v-select>  
          </v-col>
        </v-row>
      </v-container>
      <small>*indica campo obligatorio</small>
      
    </v-card-text>
    <v-card-actions>
      <div class="flex-grow-1"></div>
      <v-btn color="blue darken-1" text @click="cancel()">Cerrar</v-btn>
      <v-btn color="blue darken-1" text @click="save()">Guardar</v-btn>
    </v-card-actions>
  </v-card> 
</template>

<script>
  export default {
    props:{
      laboratory:{
        type:Object,
        required:true
      },
    },
    data() {
      return {
       labObj: Object.assign({}, this.laboratory) 
      }
    },
    watch: {
     //este watcher nos ayuda a actualizar 
     //el producto enviado desde el componente padre
      laboratory(newValue, oldValue) {        
        this.labObj = Object.assign({}, newValue)
      }
    },
    methods: {
      save() {
        this.$emit('save',{laboratory:this.labObj})
      },
      cancel(){
        this.$emit('cancel',false)
      }
    },
  }
</script>

