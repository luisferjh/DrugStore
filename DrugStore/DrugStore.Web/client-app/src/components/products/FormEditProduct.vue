<template>

    <v-card>
      <v-card-title>
        <span class="headline">Editar Producto</span>
      </v-card-title>
      <v-card-text>
        <v-form v-model="valid">      
          <v-container>
            <v-row>
              <v-col cols="12" sm="6" md="12">
                <v-text-field label="Nombre del producto*" v-model="productObj.productName" required></v-text-field>
              </v-col>
              <v-col cols="12" sm="6" md="5">
                <v-select
                  v-model="productObj.idCategory"
                  :items="categories"
                  item-text="name"
                  item-value="idCategory"
                  label="Categoria*"
                  required
                ></v-select>                        
              </v-col>
              <v-col cols="12" sm="6" md="5">
                <v-select
                  v-model="productObj.idLaboratory"
                  :items="laboratories"
                  item-text="laboratoryName"
                  item-value="idLaboratory"
                  label="Laboratorio*"
                  required
                ></v-select>                         
              </v-col>
              <v-col cols="12" md="2">
                <v-text-field label="Stock*" v-model="productObj.stock" required></v-text-field>
              </v-col>
              
              <v-col cols="12">
                <v-text-field label="Codigo de barra*" v-model="productObj.barCode"></v-text-field>
              </v-col>                     

                <v-col cols="12">
                <v-text-field label="Indicativo*" v-model="productObj.indicative" ></v-text-field>
              </v-col>

              <v-col cols="12">
                <v-text-field label="Precio*" v-model="productObj.price" required></v-text-field>
              </v-col>
              <v-col cols="12">
                
                <v-select
                  v-model="productObj.condition"
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
        </v-form>
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
    props: {     
      product:{
        type:Object,
        required:true
      },
      categories:{
        type:Array,
        required:true
      },
      laboratories:{
        type:Array,
        required:true
      }
    },
    data() {
      return {
        productObj: Object.assign({}, this.product)
      }
    },
   watch: {
     //este watcher nos ayuda a actualizar 
     //el producto enviado desde el componente padre
     product(newValue, oldValue) {        
       this.productObj = Object.assign({}, newValue)
     }
   },
    methods: {
      save() {
        this.$emit('save',{product:this.productObj})
      },
      cancel(){
        this.$emit('cancel',false)
      }
    },
  }
</script>

